import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Post } from '../models/post.model';
import { Thread } from '../models/thread.model';
import { getHttpBaseUrl } from '../util';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {

  private httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  getThreadAndPosts(id: string): Observable<ThreadContext> {
    return this.httpClient.get<ThreadContext>(`${getHttpBaseUrl()}/api/threads/${id}/posts`, {responseType: "json"});
  }

  createPost(threadId: string, content: string): Observable<Post[]> {
    return this.httpClient.post<Post[]>(`${getHttpBaseUrl()}/api/threads/${threadId}/posts`, {
      content
    }, {
      withCredentials: true
    });
  }

  createThread(boardId: string, title: string, content: string): Observable<string> {
    return this.httpClient.post<string>(`${getHttpBaseUrl()}/api/boards/${boardId}/threads`, {
      title,
      content
    }, {
      withCredentials: true,
    });
  }

  deleteThread(threadId: string): Observable<boolean> {
    const result = new Subject<boolean>();
    this.httpClient.delete(`${getHttpBaseUrl()}/api/threads/${threadId}`, {
      withCredentials: true
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

export interface ThreadContext {
  thread: Thread,
  posts: Post[]
}
