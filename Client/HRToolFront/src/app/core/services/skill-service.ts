import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateSkillDTO } from '../DTO/requests/create-skill.dto';
import { SkillDTO } from '../DTO/responses/skill.dto';

@Injectable({
    providedIn: 'root'
})
export class SkillService {
    private apiUrl = 'http://localhost:5250/api/skills';

    constructor(private http: HttpClient) { }

    createSkill(skill: CreateSkillDTO): Observable<any> {
        return this.http.post(this.apiUrl, skill);
    }

    getAllSkills(): Observable<SkillDTO[]> {
        return this.http.get<SkillDTO[]>(this.apiUrl);
    }

    deleteSkill(id: string): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}