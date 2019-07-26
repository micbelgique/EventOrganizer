import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PhotoComponent } from './photo/photo.component';
import { SpeakersComponent } from './speakers/speakers.component';
import { ProblemComponent } from './problem/problem.component';
import { ProfileComponent } from './profile/profile.component';
import { MapComponent } from './map/map.component';
import { TeamsComponent } from './teams/teams.component';

const routes: Routes = [
  {path: 'photo', component: PhotoComponent},
  {path: 'speakers', component: SpeakersComponent},
  {path: 'problem', component: ProblemComponent},
  {path: 'profile', component: ProfileComponent},
  {path: 'map', component: MapComponent},
  {path: 'teams', component: TeamsComponent},
  {path: '**', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
