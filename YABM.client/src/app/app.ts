import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainPage } from '../main-page/main-page';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MainPage , MatToolbarModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'YABM.client';
}
