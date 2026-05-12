import { Routes } from '@angular/router';
import { ManageSkills } from './features/manage-skills/manage-skills';
import { CreateCandidate } from './features/create-candidate/create-candidate';
import { ManageCandidates } from './features/manage-candidates/manage-candidates';

export const routes: Routes = [
    { path: 'manage-skills', component: ManageSkills },
    { path: 'create-candidate', component: CreateCandidate },
    { path: 'manage-candidates', component: ManageCandidates }
];
