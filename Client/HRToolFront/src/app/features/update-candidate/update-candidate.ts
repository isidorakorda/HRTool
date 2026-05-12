import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SkillService } from '../../core/services/skill-service';
import { CandidateService } from '../../core/services/candidate-service';

@Component({
  selector: 'app-update-candidate',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './update-candidate.html',
  styleUrl: './update-candidate.css',
})
export class UpdateCandidate {
  @Input() candidate: any;
  @Output() close = new EventEmitter<void>();
  @Output() updated = new EventEmitter<void>();

  allSkills: any[] = [];
  selectedSkillIds: string[] = [];

  constructor(
    private skillService: SkillService,
    private candidateService: CandidateService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadSkills();
    if (this.candidate && this.candidate.skills) {
      this.selectedSkillIds = this.candidate.skills.map((s: any) => s.id);
      this.cdr.detectChanges();
    }
  }

  loadSkills() {
    this.skillService.getAllSkills().subscribe(data => {
      this.allSkills = data
      this.cdr.detectChanges();
    });
  }

  toggleSkill(skillId: string) {
    const index = this.selectedSkillIds.indexOf(skillId);
    if (index > -1) {
      this.selectedSkillIds.splice(index, 1);
    } else {
      this.selectedSkillIds.push(skillId);
    }
  }

  onSave() {
    this.candidateService.updateCandidate(this.candidate.id, this.selectedSkillIds).subscribe({
      next: () => {
        this.updated.emit();
        this.close.emit();
      },
      error: (err) => console.error(err)
    });
  }

  onCancel() {
    this.close.emit();
  }
}
