import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


import {environment} from "../../environments/environment";
import {RegisterUser} from "../models/register-user";

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }


  register(user: RegisterUser) {
    console.log(user);
    return this.http.post(`${environment.apiUrl}/register`, user);
  }


}
