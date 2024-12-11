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
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { UnionService } from './services/api-services/union.service';
import { TeamButtonComponent } from './shared/team-button/team-button.component';
import { TaskButtonComponent } from './shared/task-button/task-button.component';
import { FormsModule } from '@angular/forms';
import { TaskInfoComponent } from './pages/task-info/task-info.component';
import { ErrorInterceptor } from './error.interceptor';
import { ErrorModalComponent } from './shared/error-modal/error-modal.component';
import { TeamDescriptionModalComponent } from './shared/team-description-modal/team-description-modal.component';
import { InvitationDescriptionModalComponent } from './shared/invitation-description-modal/invitation-description-modal.component';
import { AddTeamModalComponent } from './shared/add-team-modal/add-team-modal.component';
import { AddInvitationModalComponent } from './shared/add-invitation-modal/add-invitation-modal.component';
import { AddCompanyModalComponent } from './shared/add-company-modal/add-company-modal.component';
import { AddTaskModalComponent } from './shared/add-task-modal/add-task-modal.component';
import { AddResourceModalComponent } from './shared/add-resource-modal/add-resource-modal.component';
import { CompanySettingsComponent } from './pages/company-settings/company-settings.component';
import { CompanyMembersComponent } from './pages/company-members/company-members.component';
import { CompanyMemberComponent } from './shared/company-member/company-member.component';
import { CompanyMemberInfoModalComponent } from './shared/company-member-info-modal/company-member-info-modal.component';

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
    ErrorModalComponent,
    TeamDescriptionModalComponent,
    InvitationDescriptionModalComponent,
    AddTeamModalComponent,
    AddInvitationModalComponent,
    AddCompanyModalComponent,
    AddTaskModalComponent,
    AddResourceModalComponent,
    CompanySettingsComponent,
    CompanyMembersComponent,
    CompanyMemberComponent,
    CompanyMemberInfoModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [UnionService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
