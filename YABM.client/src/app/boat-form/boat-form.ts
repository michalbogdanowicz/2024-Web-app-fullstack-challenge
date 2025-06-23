import { Component, inject, input, output } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { BoatService } from '../../boat-service';
import { Boat } from '../../main-page/main-page';
import { FormsModule, UntypedFormBuilder } from "@angular/forms"

@Component({
  selector: 'app-boat-form',
  imports: [MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, FormsModule],
  templateUrl: './boat-form.html',
  styleUrl: './boat-form.css'
})
export class BoatForm {

  public boatName: string = "";
  public boatDescription: string = "";
  public boatId = -1;
  workDone = output<boolean>();
  private boatServiceClient = inject(BoatService);
  mode = input.required<BoatFormMode>();
  inputBoat = input<Boat>();

  readonly BoatFormMode = BoatFormMode;

  ngOnInit() {
    if (this.mode() == BoatFormMode.edit) {
      if (this.inputBoat() !== undefined) {
        this.boatName = this.inputBoat()!.name;
        this.boatDescription = this.inputBoat()!.description;
        this.boatId = this.inputBoat()!.id;
      }

    }
  }

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

  isCreationMode() {
    return this.mode() == BoatFormMode.creation;
  }

  editBoat() {
    // TODO disable button for editing the boat when inputs have not been changed.
  let sub = this.boatServiceClient.editBoat(new Boat(this.boatId, this.boatName, this.boatDescription))
      .subscribe({
        next: (data) => {
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

export enum BoatFormMode {
  creation = 0,
  edit = 1
}