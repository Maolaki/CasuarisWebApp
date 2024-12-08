import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { AllTasksComponent } from './pages/all-tasks/all-tasks.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { InvitesComponent } from './pages/invites/invites.component';
import { TeamsComponent } from './pages/teams/teams.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { AuthorizationComponent } from './pages/authorization/authorization.component';
import { HeaderComponent } from './shared/header/header.component';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { InviteButtonComponent } from './shared/invite-button/invite-button.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { HttpClientModule } from '@angular/common/http';
import { UnionService } from './services/api-services/union.service';
import { TeamButtonComponent } from './shared/team-button/team-button.component';
import { TaskButtonComponent } from './shared/task-button/task-button.component';
import { FormsModule } from '@angular/forms';
import { TaskInfoComponent } from './pages/task-info/task-info.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AllTasksComponent,
    ProfileComponent,
    InvitesComponent,
    TeamsComponent,
    RegistrationComponent,
    AuthorizationComponent,
    TaskInfoComponent,


    HeaderComponent,
    NavigationComponent,
    InviteButtonComponent,
    PaginationComponent,
    TeamButtonComponent,
    TaskButtonComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [UnionService],
  bootstrap: [AppComponent]
})
export class AppModule { }
