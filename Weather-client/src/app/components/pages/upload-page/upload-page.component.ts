import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherService } from 'src/app/services/weather.service';

@Component({
  selector: 'app-upload-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upload-page.component.html',
  styleUrls: ['./upload-page.component.scss'],
  host: {
    class: 'upload-page-component'
  }
})
export class UploadPageComponent {

  public selectedFiles: File[] = [];

  constructor(private weatherService: WeatherService) { }

  public onFilesSelected(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files) {
      const selectedFilesList: FileList = inputElement.files;
      this.selectedFiles = Array.from(selectedFilesList);
    }
  }

  public removeFile(file: File) {
    const index = this.selectedFiles.indexOf(file);
    if (index !== -1) {
      this.selectedFiles.splice(index, 1);
    }
  }

  public submitFiles(): void {
    console.log(this.selectedFiles);
    this.weatherService.createWeathersFromFiles(this.selectedFiles).subscribe({
      next: (responce) => {
        console.log(responce);
      }
    });
  }
}
