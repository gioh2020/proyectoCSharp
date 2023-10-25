import { Component } from '@angular/core';
import { ProjectServiceService } from './Services/project-service.service';
import { satelite, MessageEncrypt, sendData } from './Models/satelite';
import { count } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SpaceComunicationSPlan';
  satelitesSelected: satelite[] = []
  satelitesList: satelite[] = [];
  messagesEncrypt: any[] = [];
  distances: number[] = [];


  constructor(private service: ProjectServiceService) {
    this.service.connectApiGet('MessageIntersect/getSatelites', (res: any) => {
      const satelitesList: satelite[] = res.body
      this.satelitesList = satelitesList
    })
  }


  sateliteSelectChange(event: any) {
    const sateliteId = event.target.value;
    const sateliteIndex = event.target.id;
    const sateliteSeleted: satelite | undefined = this.satelitesList?.find((satelite: satelite) => satelite.sateliteId == sateliteId)

    if (sateliteSeleted) this.satelitesSelected[sateliteIndex] = sateliteSeleted;

  }

  distanceChange(index:number, event:any){
      const distance: number = event.target.value
      this.distances[index] = distance
 
  }

  //funcion para 
  saveData() {
    const data: sendData = {
      satelites: this.satelitesSelected,
      messages: [] ,
    }
    this.satelitesSelected.forEach((satelite: satelite, index: number) => {
      const messageEncrypt: MessageEncrypt = {
        sateliteIdRef: satelite.sateliteId,
        distance: this.distances[index],
        message: this.messagesEncrypt[index],
      }
      data.messages[index] = messageEncrypt
    });

    this.service.connectApiPost('MessageIntersect', data, (res: any) => {
      console.log(res)
    })
  }

  encryptMessage(mensaje: any) {
    const palabras = mensaje.target.value.split(' ');

    this.messagesEncrypt[0] = [...palabras];
    this.messagesEncrypt[1] = [...palabras];
    this.messagesEncrypt[2] = [...palabras];

    let count1 = 0;
    let count2 = 1;
    let count3 = 2;

    for (let i = 0; i < palabras.length; i++) {

      if (count1 < palabras.length) {
        this.messagesEncrypt[0][count1] = '';
        count1 = count1 + 3
      }
      if (count2 < palabras.length) {
        this.messagesEncrypt[1][count2] = '';
        count2 = count2 + 2
      }
      if (count3 < palabras.length) {
        this.messagesEncrypt[2][count3] = '';
        count3 = count3 + 2
      }
    }
  }
}
