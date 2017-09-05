import { Component, Inject, AfterViewInit, Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { AuthenticationService } from '../../service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Component({
    selector: 'viewcourse',
    templateUrl: './viewcourse.component.html',
    styleUrls: ['./viewcourse.component.css']
})

@Injectable()
export class ViewCourseComponent {
    headers: Headers;
    baseUrl: string;
    enrolledId: string;
    enrollment: Enrollment;
    errorMessage: String;
    completeness: number = 0;
    currentModule: any;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string, private authenticationService: AuthenticationService, private route: ActivatedRoute, private router: Router) {
        this.baseUrl = baseUrl;
        this.headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'q=0.8;application/json;q=0.9'
        });
    }

    // Node Events
    ngOnInit() {
        this.route
            .queryParams
            .subscribe(params => {
                // Defaults to 0 if no query param provided.
                if (!params['enrolledId']) {
                    this.router.navigate(['courses']);
                }

                this.enrolledId = params['enrolledId'];
            });
    }

    ngAfterViewInit() {
        if (this.authenticationService.currentUser() == '') {
            this.router.navigate(['home']);
        }

        this.fillEnrollment()
            .subscribe(enrollment => { this.enrollment = enrollment; this.completenessCal(); this.processEnrollModule(); this.lastModuleLoad(); },
            error => this.errorMessage = <any>error);

    }

    // Public Methods
    fillEnrollment(): Observable<Enrollment> {
        let options = new RequestOptions({ headers: this.headers, params: { enrolledid: this.enrolledId } });

        return this.http.get(this.baseUrl + 'api/PlugData/EnrolledCourse', options)
            .map(this.extractData)
            .catch(this.handleErrorObservable);
    }

    fillModule(courceId: string, moduleid: string): Observable<Module> {
        let options = new RequestOptions({ headers: this.headers, params: { courceId: courceId, moduleid: moduleid } });

        return this.http.get(this.baseUrl + 'api/PlugData/Module', options)
            .map(this.extractData)
            .catch(this.handleErrorObservable);
    }

    completenessCal(): void {
        if (this.enrollment.completedEnrollModulesCount == 0) {
            this.completeness = 0;
        }
        else {
            this.completeness = (this.enrollment.completedEnrollModulesCount / this.enrollment.enrollModulesCount * 100);
        }
    }

    backToCourses(): void {
        this.router.navigate(['courses']);
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

    progress(started: boolean, completed: boolean): string {
        if (completed) {
            return 'list-group-item-success';
        }

        if (started) {
            return 'list-group-item-warning';
        }

        return '';
    }

    progressIcon(started: boolean, completed: boolean): string {
        if (completed) {
            return 'glyphicon-ok-circle';
        }

        if (started) {
            return 'glyphicon-adjust';
        }

        return '';
    }

    processEnrollModule(): void {
        if (!this.enrollment) {
            return;
        }

        let modules = this.enrollment.enrollModules;

        modules.sort(function (a, b) {
            return (a.module.order > b.module.order) ? 1 : ((b.module.order > a.module.order) ? -1 : 0);
        });
    }

    isNext(): boolean {
        if (!this.currentModule) {
            return false;
        }

        let nextModule;

        for (var i = 0; i < this.enrollment.enrollModules.length; i++) {
            if (this.enrollment.enrollModules[i].module.order == this.currentModule.order + 1) {
                nextModule = this.enrollment.enrollModules[i];
            }
        }

        if (nextModule) {
            return true;
        }

        return false;
    }

    activeModule(moduleid: string): string {
        if (!this.currentModule) {
            return '';
        }

        if (this.currentModule.id == moduleid) {
            return 'active';
        }

        return '';
    }

    nextModule(): void {
        if (!this.currentModule) {
            return;
        }

        let nextModule;

        for (var i = 0; i < this.enrollment.enrollModules.length; i++) {
            if (this.enrollment.enrollModules[i].module.order == this.currentModule.order + 1) {
                nextModule = this.enrollment.enrollModules[i];
            }
        }

        if (!nextModule) {
            return;
        }

        nextModule.completedEnrollmentId = nextModule.enrollmentId;
        nextModule.completedModuleId = this.currentModule.id;

        this.showModule(nextModule, this.enrollment.courseId);
    }

    isLast(): boolean {
        if (!this.currentModule) {
            return false;
        }

        let nextModule;

        for (var i = 0; i < this.enrollment.enrollModules.length; i++) {
            if (this.enrollment.enrollModules[i].module.order == this.currentModule.order + 1) {
                nextModule = this.enrollment.enrollModules[i];
            }
        }

        if (nextModule) {
            return false;
        }

        return true;
    }

    completeModule(): void {
        if (!this.currentModule) {
            return;
        }

        let module;

        for (var i = 0; i < this.enrollment.enrollModules.length; i++) {
            if (this.enrollment.enrollModules[i].module.id == this.currentModule.id) {
                module = this.enrollment.enrollModules[i];
            }
        }

        if (!module) {
            return;
        }

        this.setCompleteModule(module, this.enrollment.courseId);
    }

    evaluate(): void {
        let evalu: boolean;
        evalu = true;

        for (var i = 0; i < this.currentModule.choices.length; i++) {
            if (!this.currentModule.choices[i].checked) {
                continue;
            }

            if (this.currentModule.choices[i].isAnswer != this.currentModule.choices[i].checked) {
                evalu = false;
            }
        }

        alert(evalu ? "Correct" : "Incorrect");
    }

    lastModuleLoad(): void {
        if (this.enrollment && this.enrollment.lastActiveModule) {
            this.showModule(this.enrollment.lastActiveModule, this.enrollment.courseId);
        }
    }

    showModule(enrollModule: EnrollModule, courseId: string): void {
        enrollModule.isStarted = true;
        let headersPost = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({
            headers: headersPost
        });

        this.http.post(this.baseUrl + 'api/PlugData/UpdateEnrollModule', enrollModule, options)
            .subscribe(result => {
                let rootObj = result.json() as RootResultObject;
                if (rootObj.result.sucess) {
                    try {
                        this.fillEnrollment()
                            .subscribe(enrollment => { this.enrollment = enrollment; this.completenessCal(); this.processEnrollModule(); },
                            error => this.errorMessage = <any>error);

                        this.fillModule(courseId, enrollModule.moduleId)
                            .subscribe(result => { this.currentModule = result; },
                            error => this.errorMessage = <any>error);
                    }
                    catch (e) {
                        this.router.navigate(['viewcourse'], { queryParams: { enrolledId: this.enrolledId } });
                    }
                }
            }, error => console.error(error));
    }

    setCompleteModule(enrollModule: EnrollModule, courseId: string): void {
        enrollModule.isCompleted = true;
        let headersPost = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({
            headers: headersPost
        });

        this.http.post(this.baseUrl + 'api/PlugData/CompleteEnrollModule', enrollModule, options)
            .subscribe(result => {
                let rootObj = result.json() as RootResultObject;
                if (rootObj.result.sucess) {
                    try {
                        this.backToCourses();
                    }
                    catch (e) {
                        this.router.navigate(['viewcourse'], { queryParams: { enrolledId: this.enrolledId } });
                    }
                }
            }, error => console.error(error));
    }

    // Private Methods
    private extractData(res: Response) {
        let body = res.json();
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

// Interfaces and Classes
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

export interface CourseModel {
    id: string;
    subject: string;
    description: string;
    enrolled: boolean;
    enrolledId?: any;
    modules?: any;
}

export interface Module {
    id: string;
    title: string;
    icon: string;
    canSkip: boolean;
    order: number;
}

export interface EnrollModule {
    enrollmentId: string;
    moduleId: string;
    isStarted: boolean;
    isCompleted: boolean;
    module: Module;
    lastVisited: Date;
    completedEnrollmentId: string;
    completedModuleId: string;
}

export interface Enrollment {
    id: string;
    courseId: string;
    courseModel: CourseModel;
    studentId: string;
    enrollModulesCount: number;
    completedEnrollModulesCount: number;
    enrollModules: EnrollModule[];
    lastSeen: string;
    lastActiveModule: EnrollModule;
}
