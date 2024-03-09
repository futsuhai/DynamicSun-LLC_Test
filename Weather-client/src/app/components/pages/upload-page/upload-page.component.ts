import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

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

  public onFilesSelected(event: any) {
    const selectedFilesList: FileList = event.target.files;
    this.selectedFiles = [];
    for (let i = 0; i < selectedFilesList.length; i++) {
      this.selectedFiles.push(selectedFilesList[i]);
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
  }

}
