import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateCandidateDTO } from '../DTO/requests/create-candidate.dto';
import { CandidateDTO } from '../DTO/responses/candidate.dto';

@Injectable({
    providedIn: 'root'
})
export class CandidateService {
    private apiUrl = 'http://localhost:5250/api/candidates';

    constructor(private http: HttpClient) { }

    createCandidate(candidate: CreateCandidateDTO): Observable<any> {
        return this.http.post(this.apiUrl, candidate);
    }

    getAllCandidates(name?: string, skillIds?: string[]): Observable<CandidateDTO[]> {
        let params = new HttpParams();
        if (name) params = params.set('name', name);
        if (skillIds && skillIds.length > 0) {
            skillIds.forEach(id => params = params.append('skillIds', id));
        }
        return this.http.get<CandidateDTO[]>(this.apiUrl, { params });
    }

    updateCandidate(id: string, skillIds: string[]): Observable<any> {
        let params = new HttpParams();
        if (skillIds && skillIds.length > 0) {
            skillIds.forEach(skillId => {
                params = params.append('skillIds', skillId);
            });
        }
        return this.http.put(`${this.apiUrl}/${id}`, null, { params });
    }

    deleteCandidate(id: string): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}