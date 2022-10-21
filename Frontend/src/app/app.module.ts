import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { MatSliderModule } from '@angular/material/slider'
import { MatIconModule } from '@angular/material/icon'

import { AppComponent } from './app.component';
import { ExampleComponent } from './pages/example/example.component';
import { BoardsComponent } from './pages/boards/boards.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BoardItemComponent } from './components/board/board-item.component';
import { BoardComponent } from './pages/board/board.component';
import { ThreadItemComponent } from './components/thread-item/thread-item.component';
import { ThreadComponent } from './pages/thread/thread.component';
import { PostItemComponent } from './components/post-item/post-item.component';

@NgModule({
  declarations: [
    AppComponent,
    ExampleComponent,
    BoardsComponent,
    BoardItemComponent,
    BoardComponent,
    ThreadItemComponent,
    ThreadComponent,
    PostItemComponent,
  ],
  imports: [
    BrowserModule, 
    HttpClientModule, 
    AppRoutingModule, 
    BrowserAnimationsModule,
    FormsModule,

    MatCardModule,
    MatButtonModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    MatSliderModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
