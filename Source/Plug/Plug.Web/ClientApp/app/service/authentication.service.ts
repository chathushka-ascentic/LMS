import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {
    public token: any;
    item: any;

    constructor(private http: Http) {
    }

    // Log Out
    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('plugData');
    }

    // Retrive User Data
    currentUserToken(): string {
        this.item = localStorage.getItem('plugData');
        let currentUser = JSON.parse(this.item);
        return currentUser.token;
    } 

    // Retrive User
    currentUser(): string {
        this.item = localStorage.getItem('plugData');

        if (!this.item)
        {
            return '';
        }

        let currentUser = JSON.parse(this.item);
        return currentUser.username;
    } 
}