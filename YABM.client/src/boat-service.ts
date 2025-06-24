import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Boat } from './main-page/main-page';

@Injectable({
  providedIn: 'root'
})
export class BoatService {

  port = 32779
  address = `https://localhost:${this.port}/Boat`;
  constructor(private http: HttpClient) {
  }

  getBoats(): Observable<Boat[]> {
    return this.http.get<Boat[]>(this.address);
  }

  deleteBoat(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.address + '?id=' + id);
  }

  createBoat(newBoat: Boat): Observable<boolean> {
    return this.http.post<boolean>(this.address, { name: newBoat.name, description: newBoat.description });
  }

  editBoat(oldBoat: Boat) {
    return this.http.put<boolean>(this.address, { id: oldBoat.id, name: oldBoat.name, description: oldBoat.description });
  }

}
