import { Component, inject, input, output } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { BoatService } from '../../boat-service';
import { Boat } from '../../main-page/main-page';
import { FormsModule } from "@angular/forms"

@Component({
  selector: 'app-boat-form',
  imports: [MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, FormsModule],
  templateUrl: './boat-form.html',
  styleUrl: './boat-form.css'
})
export class BoatForm {
  // to make a value necessary from the Boat Form.
  public boatName: string = "";
  public boatDescription: string = "";
  workDone = output<boolean>();
  private boatServiceClient = inject(BoatService);

  cancel() {
    this.workDone.emit(false);

  }

  createBoat() {
    //TODO disable buttons.
    if (this.boatName.trim() == "") {
      // TODO actually show this error.
      throw new Error("Name cannot be empty");
    }
    // TODO more validaiton of length, could  be done in the material settings probably.
    let sub = this.boatServiceClient.createBoat(new Boat(0, this.boatName, this.boatDescription))
      .subscribe({
        next: (data) => {
          console.log("object created")
          this.workDone.emit(true);

        },
        error: (error) => {
          console.log(error)
          sub.unsubscribe()
        },
        complete: () => {
          sub.unsubscribe()
        }
      })
  }
}
