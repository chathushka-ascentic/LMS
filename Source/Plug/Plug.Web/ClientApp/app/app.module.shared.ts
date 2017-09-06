import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { CoursesComponent } from './components/courses/courses.component';
import { ViewCourseComponent } from './components/viewcourse/viewcourse.component';
import { ManageCoursesComponent } from './components/managecourses/managecourses.component';


import { AuthenticationService } from './service/authentication.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        CoursesComponent,
        ViewCourseComponent,
        ManageCoursesComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'courses', component: CoursesComponent },
            { path: 'viewcourse', component: ViewCourseComponent },
            { path: 'managecourses', component: ManageCoursesComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AuthenticationService
    ]
})
export class AppModuleShared {
}
