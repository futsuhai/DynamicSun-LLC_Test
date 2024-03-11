import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherService } from 'src/app/services/weather.service';
import { IFileUploadInfo } from 'src/app/models/FileUploadInfo.model';

@Component({
  selector: 'app-upload-page',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upload-page.component.html',
  styleUrls: ['./upload-page.component.scss'],
  host: {
    class: 'upload-component'
  }
})
export class UploadPageComponent {

  public errorMessage: string = "";
  public selectedFiles: File[] = [];
  public filesUploadInfo: IFileUploadInfo[] = [];

  constructor(private weatherService: WeatherService) { }

  public onFilesSelected(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    this.errorMessage = "";
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
    this.weatherService.createWeathersFromFiles(this.selectedFiles).subscribe({
      next: (filesUploadInfo: IFileUploadInfo[]) => {
        this.filesUploadInfo = filesUploadInfo;
        if (this.filesUploadInfo.some(file => !file.result)) {
          this.errorMessage = "Один из загруженных файлов не подлежит разбору!";
        } else {
          this.errorMessage = "Все файлы успешно загружены";
        }
      }
    });
  }
}
