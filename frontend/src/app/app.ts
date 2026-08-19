import { Component, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly status = signal<'checking' | 'connected' | 'error'>('checking');
  protected readonly message = signal('');

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<{ message: string }>(`${environment.apiUrl}/api/hello`).subscribe({
      next: (res) => {
        this.status.set('connected');
        this.message.set(res.message);
      },
      error: () => {
        this.status.set('error');
        this.message.set('Could not reach the API.');
      },
    });
  }
}
