import { Component, Inject } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from '../../model/student';
import { Output } from '../../model/output';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent {
    student: Student;
    headers: Headers;
    baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
        this.baseUrl = baseUrl;
        this.student = new Student();
        this.headers = new Headers({
            'Content-Type': 'application/json',
            'Accept': 'q=0.8;application/json;q=0.9'
        });
    }

    // Public Methods
    login() {
        let headersPost = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({
            headers: headersPost
        });

        this.http.post(this.baseUrl + 'api/PlugData/VerifyStudent', this.student, options)
            .subscribe(result => {
                console.log(result);
                let user = result.json() as Output;
                localStorage.setItem('plugData', JSON.stringify({ username: user.result.email, token: user.result.id }));
                this.router.navigate(['courses']);
            }, error => console.error(error));
    }
} 
