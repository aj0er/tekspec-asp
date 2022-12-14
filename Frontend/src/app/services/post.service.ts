import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Post } from '../models/post.model';
import { getHttpBaseUrl } from '../util';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  deletePost(id: string): Observable<boolean> {
    const result = new Subject<boolean>();

    this.httpClient.delete(`${getHttpBaseUrl()}/Posts/${id}`, {
      withCredentials: true,
    }).subscribe({
        error: (_) => {
          result.next(false);
          result.complete();
        },
        next: () => {
          result.next(true);
          result.complete();
        }
    });

    return result.asObservable();
  }

  saveEditedPost(postId: string, request: {content: string}): Observable<boolean> {
    const result = new Subject<boolean>();

    this.httpClient.put(`${getHttpBaseUrl()}/Posts/${postId}`, request, {
      withCredentials: true,
    }).subscribe({
        error: (_) => {
          result.next(false);
          result.complete();
        },
        next: () => {
          result.next(true);
          result.complete();
        }
    });

    return result.asObservable();
  }

}
