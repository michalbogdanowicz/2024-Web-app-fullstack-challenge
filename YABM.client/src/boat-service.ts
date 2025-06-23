import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Boat } from './main-page/main-page';

@Injectable({
  providedIn: 'root'
})
export class BoatService {


 constructor(private http: HttpClient) {}

 getBoats(): Observable<Boat[]>{
    return this.http.get<Boat[]>('https://localhost:32769/Boat');
 }

  deleteBoat(id: number) : Observable<boolean> {
    return this.http.delete<boolean>('https://localhost:32769/Boat?id=' + id);
 }

   createBoat(newBoat : Boat) : Observable<boolean> {
    return this.http.post<boolean>('https://localhost:32769/Boat', { name : newBoat.name, description : newBoat.description });
 }
}
