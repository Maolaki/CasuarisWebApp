import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './pages/profile/profile.component';
import { InvitesComponent } from './pages/invites/invites.component';
import { HomeComponent } from './pages/home/home.component';
import { TeamsComponent } from './pages/teams/teams.component';
import { AllTasksComponent } from './pages/all-tasks/all-tasks.component';
import { TaskInfoComponent } from './pages/task-info/task-info.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { AuthorizationComponent } from './pages/authorization/authorization.component';
import { CompanySettingsComponent } from './pages/company-settings/company-settings.component';
import { CompanyMembersComponent } from './pages/company-members/company-members.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'authorization', component: AuthorizationComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'invites', component: InvitesComponent },
  { path: 'teams', component: TeamsComponent },
  { path: 'all-tasks', component: AllTasksComponent },
  { path: 'task-info', component: TaskInfoComponent },
  { path: 'company-settings', component: CompanySettingsComponent },
  { path: 'company-members', component: CompanyMembersComponent },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
