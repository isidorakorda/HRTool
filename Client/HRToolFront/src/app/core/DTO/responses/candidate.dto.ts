import { SkillDTO } from "./skill.dto";

export interface CandidateDTO {
    id: string,
    name: string,
    dateOfBirth: Date,
    phone: string,
    email: string,
    skills: SkillDTO[]
}