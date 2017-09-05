import { Component, Inject, AfterViewInit, Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { AuthenticationService } from '../../service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Component({
    selector: 'courses',
    templateUrl: './courses.component.html',
    styleUrls: ['./courses.component.css']
})

@Injectable()
export class CoursesComponent {
    headers: Headers;
    baseUrl: string;
    courses: Course[];
    errorMessage: String;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string, private authenticationService: AuthenticationService, private route: ActivatedRoute, private router: Router) {
        this.baseUrl = baseUrl;
        this.headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'q=0.8;application/json;q=0.9'
        });
    }

    // Node Events
    ngAfterViewInit() {
        if (this.authenticationService.currentUser() == '') {
            this.router.navigate(['home']);
        }

        this.fillCourses()
            .subscribe(courses => this.courses = courses,
            error => this.errorMessage = <any>error);
    }

    // Public Events
    fillCourses(): Observable<Course[]> {
        let options = new RequestOptions({ headers: this.headers, params: { studetnid: this.authenticationService.currentUserToken() } });

        return this.http.get(this.baseUrl + 'api/PlugData/Courses', options)
            .map(this.extractData)
            .catch(this.handleErrorObservable);
    }

    backToHome(): void {
        this.authenticationService.logout();
        this.router.navigate(['home']);
    }

    enroll(course: any) {
        let enrollment = {
            StudentId: this.authenticationService.currentUserToken(),
            CourseId: course.id
        };
        let headersPost = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({
            headers: headersPost
        });

        this.http.post(this.baseUrl + 'api/PlugData/EnrollCourse', enrollment, options)
            .subscribe(result => {
                let rootObj = result.json() as RootResultObject;
                if (rootObj.result.sucess) {
                    this.fillCourses()
                        .subscribe(courses => this.courses = courses,
                        error => this.errorMessage = <any>error);
                }
            }, error => console.error(error));
    }

    viewCoursePage(course: any) {
        this.router.navigate(['viewcourse'], { queryParams: { enrolledId: course.enrolledId } });
    }

    // Private Events
    private extractData(res: Response) {
        let body = res.json() as RootObject;
        return body.result || {};
    }

    private handleErrorObservable(error: Response | any) {
        console.error(error.message || error);
        return Observable.throw(error.message || error);
    }

    private handleErrorPromise(error: Response | any) {
        console.error(error.message || error);
        return Promise.reject(error.message || error);
    }

}

// Interfaces And Classes
export interface Choice {
    option: string;
    isAnswer: boolean;
}

export interface Module {
    icon: string;
    uri: string;
    duration: string;
    id: string;
    title: string;
    canSkip: boolean;
    description: string;
    text: string;
    choices: Choice[];
}

export interface Course {
    id: string;
    subject: string;
    description: string;
    enrolled: boolean;
    enrolledId: string;
    modules: Module[];
}

export interface RootObject {
    result: Course[];
    sucess: boolean;
    additionalMessage?: any;
    error?: any;
}

export interface Result {
    sucess: boolean;
    additionalMessage?: any;
    error?: any;
}

export interface RootResultObject {
    result: Result;
    id: number;
    exception?: any;
    status: number;
    isCanceled: boolean;
    isCompleted: boolean;
    isCompletedSuccessfully: boolean;
    creationOptions: number;
    asyncState?: any;
    isFaulted: boolean;
}