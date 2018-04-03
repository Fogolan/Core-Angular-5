import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

import { Credentials, LoginContext } from './authentication.service';

export class MockAuthenticationService {

  credentials: Credentials | null = {
    id: 'test',
    token: '123',
    username: 'test',
    role: 'user'
  };

  login(context: LoginContext): Observable<Credentials> {
    return of({
      id: context.username,
      token: '123456',
      username: 'test',
      role: 'user'
    });
  }

  logout(): Observable<boolean> {
    this.credentials = null;
    return of(true);
  }

  isAuthenticated(): boolean {
    return !!this.credentials;
  }

}
