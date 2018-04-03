import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from '@app/core';

const Bearer = 'Bearer ';

@Injectable()
export class HttpRequestTokenInterceptor implements HttpInterceptor {

    constructor(private injector: Injector) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const authenticationService = this.injector.get(AuthenticationService);
        if (!authenticationService.isAuthenticated()) {
            return next.handle(request);
        }

        request = request.clone({
             setHeaders: {
                  Authorization: Bearer + authenticationService.credentials.token
                }
            });

        return next.handle(request);
    }
}