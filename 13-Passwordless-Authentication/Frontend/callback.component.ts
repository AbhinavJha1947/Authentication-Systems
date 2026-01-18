import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-callback',
    template: '<p>Processing login...</p>'
})
export class CallbackComponent implements OnInit {
    constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) { }

    ngOnInit() {
        // Extract token from URL
        const token = this.route.snapshot.queryParamMap.get('token');

        if (token) {
            this.http.get<{ accessToken: string }>(`/api/callback?token=${token}`)
                .subscribe({
                    next: (res) => {
                        localStorage.setItem('access_token', res.accessToken);
                        this.router.navigate(['/dashboard']);
                    },
                    error: () => this.router.navigate(['/login'])
                });
        }
    }
}
