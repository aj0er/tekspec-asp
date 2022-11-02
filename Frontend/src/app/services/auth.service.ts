import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AuthenticatedUser } from '../models/auth.model';
import { getHttpBaseUrl } from '../util';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private httpClient: HttpClient;
  user = new BehaviorSubject<AuthenticatedUser | null>(null);

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  authenticate() {
    this.httpClient.get<AuthenticatedUser>(`${getHttpBaseUrl()}/api/auth/me/`, {
        withCredentials: true
    }).subscribe({next: (v) => this.user.next(v)});
  }
}
