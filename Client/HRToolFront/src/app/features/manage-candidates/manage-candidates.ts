import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CandidateService } from '../../core/services/candidate-service';
import { CandidateDTO } from '../../core/DTO/responses/candidate.dto';
import { FormsModule } from '@angular/forms';
import { debounceTime } from 'rxjs';
import { Subject, Subscription } from 'rxjs';
import { SkillService } from '../../core/services/skill-service';
import { UpdateCandidate } from '../update-candidate/update-candidate';

@Component({
  selector: 'manage-candidates',
  standalone: true,
  imports: [CommonModule, FormsModule, UpdateCandidate],
  templateUrl: './manage-candidates.html',
  styleUrls: ['./manage-candidates.css']
})
export class ManageCandidates implements OnInit, OnDestroy {
  showUpdatePopup = false;
  selectedCandidate: any = null;

  candidates: CandidateDTO[] = [];
  skills: any[] = [];

  searchName: string = ''
  selectedSkillIds: string[] = []

  private searchSubject = new Subject<void>();
  private searchSubscription?: Subscription;

  constructor(
    private skillService: SkillService,
    private candidateService: CandidateService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadSkills();
    this.loadCandidates();

    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(200)
    ).subscribe(() => {
      this.loadCandidates();
      this.cdr.detectChanges();
    });
  }

  onFilterChange() {
    this.searchSubject.next();
  }

  toggleSkill(skillId: string) {
    const index = this.selectedSkillIds.indexOf(skillId);
    if (index > -1) {
      this.selectedSkillIds.splice(index, 1);
    } else {
      this.selectedSkillIds.push(skillId);
    }
    this.onFilterChange();
  }

  onSearchChange() {
    this.searchSubject.next();
  }

  loadCandidates() {
    this.candidateService.getAllCandidates(this.searchName, this.selectedSkillIds).subscribe({
      next: (data) => {
        this.candidates = data
        this.cdr.detectChanges()
      }
    });
  }

  loadSkills() {
    this.skillService.getAllSkills().subscribe({
      next: (data) => {
        this.skills = data
        this.cdr.detectChanges()
      },
      error: (err) => console.error(err)
    });
  }

  ngOnDestroy() {
    this.searchSubscription?.unsubscribe();
  }

  onEdit(candidate: CandidateDTO) {
    this.selectedCandidate = { ...candidate };
    this.showUpdatePopup = true;
  }

  closePopup() {
    this.showUpdatePopup = false;
    this.selectedCandidate = null;
  }

  handleUpdateSuccess() {
    this.closePopup();
    this.loadCandidates();
  }

  onDelete(id: string) {
    if (confirm('Are you sure you want to delete this candidate?')) {
      this.candidateService.deleteCandidate(id).subscribe({
        next: () => this.loadCandidates(),
        error: (err) => console.error(err)
      })
    }
  }
}