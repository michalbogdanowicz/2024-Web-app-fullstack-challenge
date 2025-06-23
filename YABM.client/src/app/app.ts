import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainPage } from '../main-page/main-page';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MainPage ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'YABM.client';
}
