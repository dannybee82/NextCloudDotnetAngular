import { inject } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { OwnCloudFileService } from "../services/own-cloud-file.service";
import { FileDownload } from "../models/file-download.interface";

export class DownloadFile {

    fileService = inject(OwnCloudFileService);
    toastr = inject(ToastrService);

    download(filename: string) : void {
        if(filename !== '') {
            this.fileService.downloadFile(filename).subscribe({
              next: (result: FileDownload) => {
                if(!result.hasErrors) {          
                  try {
                    console.log(result);
                    let link = document.createElement('a');
                    link.href = `data:${result.fileMimeType};base64,` + result.fileBase64;
                    //link.href = result.fileBase64;
                    link.download = result.fileName ?? 'unknown_download';
                    link.dispatchEvent(new MouseEvent('click'));
            
                    this.toastr.success('Bestand gedownload.');
                  } catch {
                    this.toastr.error('Kan bestand niet downloaden');
                  }          
                } else {
                  this.toastr.error(result.errorMessage ?? 'Kan bestand niet downloaden');
                }
              },
              error: () => {
                this.toastr.error('Kan bestand niet downloaden');
              }
            });
        } else {
            this.toastr.error('Kan bestand niet downloaden. Bestand heeft geen naam.');
        }
    };

}