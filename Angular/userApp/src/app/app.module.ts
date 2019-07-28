import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SpeakersComponent } from './speakers/speakers.component';
import { PhotoComponent } from './photo/photo.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { HttpClientModule} from '@angular/common/http';
import { NotifierModule } from 'angular-notifier';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatInputModule,
  MatCardModule,
  MatSidenavModule,
  MatListModule,
  MatProgressSpinnerModule,
  MatExpansionModule,
  MatGridListModule,
  MatToolbarModule,
  MatDividerModule
} from '@angular/material';
import { FormsModule } from '@angular/forms';
import { ProblemComponent } from './problem/problem.component';
import { ProfileComponent } from './profile/profile.component';
import { MapComponent } from './map/map.component';
import { TeamsComponent } from './teams/teams.component';
import { OngComponent } from './ong/ong.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SpeakersComponent,
    PhotoComponent,
    ProblemComponent,
    ProfileComponent,
    MapComponent,
    TeamsComponent,
    OngComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    HttpClientModule,
    MatInputModule,
    FormsModule,
    MatCardModule,
    MatSidenavModule,
    MatListModule,
    MatProgressSpinnerModule,
    MatExpansionModule,
    MatGridListModule,
    NotifierModule,
    MatToolbarModule,
    MatDividerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
