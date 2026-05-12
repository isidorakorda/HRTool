import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SkillService } from '../../core/services/skill-service';
import { SkillDTO } from '../../core/DTO/responses/skill.dto';

@Component({
  selector: 'app-manage-skills',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './manage-skills.html',
  styleUrls: ['./manage-skills.css']
})
export class ManageSkills implements OnInit {

  skills: SkillDTO[] = [];
  skillForm = new FormGroup({
    name: new FormControl('', [Validators.required])
  });

  constructor(
    private skillsService: SkillService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadSkills();
  }

  loadSkills() {
    this.skillsService.getAllSkills().subscribe({
      next: (data) => {
        this.skills = data;
        this.cdr.detectChanges()
      },
      error: (err) => {
        console.error('Error loading skills', err);
        this.cdr.detectChanges();
      }
    });
  }

  onSubmit() {
    if (this.skillForm.valid) {
      const dto = { name: this.skillForm.value.name! };
      this.skillsService.createSkill(dto).subscribe({
        next: () => {
          this.skillForm.reset();
          this.loadSkills();
        }
      });
    }
    this.cdr.detectChanges();
  }

  onDelete(id: string) {
    if (confirm('Are you sure you want to delete this skill?')) {
      this.skillsService.deleteSkill(id).subscribe({
        next: () => this.loadSkills(),
        error: (err) => console.error('Error deleting skill', err)
      });
    }
    this.cdr.detectChanges();
  }
}