import { Component, Inject, AfterViewInit, Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { AuthenticationService } from '../../service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Component({
    selector: 'managecourses',
    templateUrl: './managecourses.component.html',
    styleUrls: ['./managecourses.component.css']
})

@Injectable()
export class ManageCoursesComponent {
    headers: Headers;
    baseUrl: string;
    courses: Course[];
    errorMessage: String;
    adding: boolean;
    newCourse: Course;
    newModule: Module;
    newChoice: Choice;
    newModules: Module[];
    moduleType: number;
    order: number;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string, private authenticationService: AuthenticationService, private route: ActivatedRoute, private router: Router) {
        this.baseUrl = baseUrl;
        this.headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'q=0.8;application/json;q=0.9'
        });
        this.adding = false;
        this.moduleType = 0;
        this.newCourse = new CourseClass();
        this.newModule = new ModuleClass();
        this.newModule.choices = new Array<ChoiceClass>();
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModules = new Array<ModuleClass>();
        this.order = 0;
    }

    // Node Events
    ngAfterViewInit() {
        this.fillCourses()
            .subscribe(courses => { this.courses = courses; },
            error => this.errorMessage = <any>error);
    }

    // Public Methods
    fillCourses(): Observable<Course[]> {
        let options = new RequestOptions({ headers: this.headers, params: { studetnid: '' } });

        return this.http.get(this.baseUrl + 'api/PlugData/Courses', options)
            .map(this.extractData)
            .catch(this.handleErrorObservable);
    }

    backToHome(): void {
        this.authenticationService.logout();
        this.router.navigate(['home']);
    }

    viewCoursePage(course: any) {
        this.router.navigate(['viewcourse'], { queryParams: { enrolledId: course.enrolledId } });
    }

    addcourse(): void {
        this.adding = true;
    }

    addModule(moduleType: number): void {
        this.moduleType = moduleType;
    }

    addModuleToCourse(): void {
        this.newModule.order = this.order;
        this.newModule.icon = this.moduleType == 1 ? "TEXT" : this.moduleType == 2 ? "VIDEO" : "QUESTION";
        this.order = this.order + 1;
        this.newModules.push(this.newModule);
        this.newModule = new ModuleClass();
        this.newModule.choices = new Array<ChoiceClass>();
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.moduleType = 0;
    }

    removeModuleFromCourse(order: number): void {
        let index = -1;
        for (let i = 0; i < this.newModules.length; i++) {
            if (this.newModules[i].order == order) {
                index = i;
            }
        }

        this.newModules.splice(index, 1);
    }

    icon(iconText: string): string {
        if (iconText == 'TEXT') {
            return 'glyphicon-list-alt';
        }

        if (iconText == 'VIDEO') {
            return 'glyphicon-play-circle';
        }

        if (iconText == 'QUESTION') {
            return 'glyphicon-pencil';
        }

        return '';
    }

    cancelModule(): void {
        this.newModule = new ModuleClass();
        this.newModule.choices = new Array<ChoiceClass>();
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.newModule.choices.push(new ChoiceClass());
        this.moduleType = 0;
    }

    addCourseToDb(): void {
        this.newCourse.modules = this.newModules;
        let headersPost = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({
            headers: headersPost
        });

        this.http.post(this.baseUrl + 'api/PlugData/AddCourse', this.newCourse, options)
            .subscribe(result => {
                let rootObj = result.json() as RootResultObject;
                if (rootObj.result.sucess) {
                    try {
                        this.fillCourses()
                            .subscribe(courses => { this.courses = courses; },
                            error => this.errorMessage = <any>error);

                        this.adding = false;
                        this.moduleType = 0;
                        this.newCourse = new CourseClass();
                        this.newModule = new ModuleClass();
                        this.newModule.choices = new Array<ChoiceClass>();
                        this.newModule.choices.push(new ChoiceClass());
                        this.newModule.choices.push(new ChoiceClass());
                        this.newModule.choices.push(new ChoiceClass());
                        this.newModule.choices.push(new ChoiceClass());
                        this.newModules = new Array<ModuleClass>();
                        this.order = 0;
                    }
                    catch (e) {
                        this.router.navigate(['managecourses']);
                    }
                }
            }, error => console.error(error));
    }

    // Private Methods
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

class ChoiceClass implements Choice {
    option: string;
    isAnswer: boolean;
}

export interface Choice {
    option: string;
    isAnswer: boolean;
}

class ModuleClass implements Module {
    icon: string;
    uri: string;
    duration: string;
    id: string;
    title: string;
    canSkip: boolean;
    order: number;
    description: string;
    text: string;
    choices: Choice[];
}

export interface Module {
    icon: string;
    uri: string;
    duration: string;
    id: string;
    title: string;
    canSkip: boolean;
    order: number;
    description: string;
    text: string;
    choices: Choice[];
}
class CourseClass implements Course {
    id: string;
    subject: string;
    description: string;
    enrolled: boolean;
    enrolledId?: any;
    modules: Module[];
}

export interface Course {
    id: string;
    subject: string;
    description: string;
    enrolled: boolean;
    enrolledId?: any;
    modules: Module[];
}
export interface RootObject {
    result: Course[];
    sucess: boolean;
    additionalMessage?: any;
    error?: any;
}