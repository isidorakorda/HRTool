import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SkillService } from '../../core/services/skill-service';
import { SkillDTO } from '../../core/DTO/responses/skill.dto';
import { CreateCandidateDTO } from '../../core/DTO/requests/create-candidate.dto';
import { CandidateService } from '../../core/services/candidate-service';

@Component({
  selector: 'app-create-candidate',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create-candidate.html',
  styleUrls: ['./create-candidate.css']
})
export class CreateCandidate implements OnInit {
  skills: SkillDTO[] = [];
  selectedSkillIds: string[] = [];

  userForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
    dateOfBirth: new FormControl('', [Validators.required]),
    phone: new FormControl('', [Validators.required, Validators.pattern('^[0-9]*$')]),
    email: new FormControl('', [Validators.required, Validators.email])
  });

  constructor(
    private candidateService: CandidateService,
    private skillService: SkillService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.skillService.getAllSkills().subscribe({
      next: (data) => {
        this.skills = data;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Error loading skills', err)
    });
  }

  onSkillChange(skillId: string, event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    if (isChecked) {
      this.selectedSkillIds.push(skillId);
    } else {
      this.selectedSkillIds = this.selectedSkillIds.filter(id => id !== skillId);
    }
  }

  onSubmit() {
    if (this.userForm.valid && this.selectedSkillIds.length > 0) {
      const formValues = this.userForm.value;

      const candidateData: CreateCandidateDTO = {
        name: formValues.name!,
        dateOfBirth: new Date(formValues.dateOfBirth!).toISOString().split('T')[0] as any,
        phone: formValues.phone!,
        email: formValues.email!,
        skillIDs: this.selectedSkillIds
      };

      this.candidateService.createCandidate(candidateData).subscribe({
        next: () => {
          this.resetForm()
          this.cdr.detectChanges()
        },
        error: (err) => console.error(err)
      });
    }
  }

  resetForm() {
    this.userForm.reset();
    this.selectedSkillIds = [];
    const checkboxes = document.querySelectorAll('.checkbox-item input[type="checkbox"]');
    checkboxes.forEach((cb) => {
      (cb as HTMLInputElement).checked = false;
    });
  }
}