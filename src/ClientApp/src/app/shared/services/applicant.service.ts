import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { ApplicantAddCommand } from '../models/ApplicantAddCommand';

@Injectable({
  providedIn: 'root',
})
export class ApplicantService {
  private serverBaseUrl = environment.serverBaseUrl;

  constructor(private http: HttpClient) {}

  getAllApplicants(): Observable<any> {
    return this.http.get(`${this.serverBaseUrl}/api/v1/applicants`);
  }

  addApplicant(applicant: ApplicantAddCommand): Observable<any> {
    return this.http.post(`${this.serverBaseUrl}/api/v1/applicants`, applicant);
  }

  editApplicant(id: number, applicant: ApplicantAddCommand): Observable<any> {
    return this.http.put(
      `${this.serverBaseUrl}/api/v1/applicants/${id}`,
      applicant
    );
  }

  deleteApplicant(id: number): Observable<any> {
    return this.http.delete(`${this.serverBaseUrl}/api/v1/applicants/${id}`);
  }
}
