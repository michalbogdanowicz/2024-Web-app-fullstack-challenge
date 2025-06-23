import { Component, signal, inject, ChangeDetectorRef, } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatExpansionModule } from '@angular/material/expansion';
import { BoatService } from '../boat-service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Alert } from '../app/alert/alert';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BoatForm } from '../app/boat-form/boat-form';


@Component({
  selector: 'app-main-page',
  imports: [MatToolbarModule, MatExpansionModule, MatDialogModule, MatButtonModule, MatIconModule, BoatForm],
  templateUrl: './main-page.html',
  styleUrl: './main-page.css'
})
export class MainPage {
  readonly panelOpenState = signal(false);
  items: Boat[] = [];
  private boatServiceClient = inject(BoatService);
  errorState: boolean = false;
  creationNeeded = false;
  private readonly cd = inject(ChangeDetectorRef);

  ngAfterContentInit() {
    this.refreshListOfBoats();
  }

  refreshListOfBoats() {
    let sub = this.boatServiceClient.getBoats().subscribe({
      next: (data) => {
        this.items.length = 0; // empty the list.
        data.forEach(i => this.items.push(i))
        console.log(this.items)
        this.cd.detectChanges();
      },
      error: (error) => {
        console.log(error)
        this.openDialog(error.message)
        this.errorState = true;
        sub.unsubscribe()
      },
      complete: () => {
        sub.unsubscribe()
      }
    })

  }

  deleteClick(id: number) {
    // TODO disable button during deletion.

    this.boatServiceClient.deleteBoat(id).subscribe({
      next: (data) => {
        this.refreshListOfBoats();
      },
      error: (error) => {
        console.log(error)
        this.openDialog(error.message)
      },
      complete: () => {
      }
    })
  }



  readonly dialog = inject(MatDialog);

  openDialog(message: string): void {
    this.dialog.open(Alert, {
      width: '250px',
      data: {
        message: message,
      },
    });
  }

  openCreationForm() {
    console.log('openCreationForm caleed');
    this.creationNeeded = true;
    this.cd.detectChanges();

  }

  closeCreationForm(bool: boolean) {
    console.log('closeCreationForm caleed');
    this.creationNeeded = false;
    console.log(bool);
    if (bool) {
      this.refreshListOfBoats();
    } else {
      this.cd.detectChanges();
    }
  }
}



export class Boat {
  id: number;
  name: string;
  description: string;

  constructor(id: number, name: string, description: string) {
    this.id = id;
    this.name = name;
    this.description = description;
  }



}