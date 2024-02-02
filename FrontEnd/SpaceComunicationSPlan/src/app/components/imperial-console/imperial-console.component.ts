import { Component } from '@angular/core';
import { ProjectServiceService } from 'src/app/Services/project-service.service';
import { satelite, MessageEncrypt, sendData } from 'src/app/Models/satelite';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-imperial-console',
  templateUrl: './imperial-console.component.html',
  styleUrls: ['./imperial-console.component.css']
})
export class ImperialConsoleComponent {

  satelitesSelected: satelite[] = []
  satelitesList: satelite[] = [];
  messagesEncrypt: any[] = [];
  distances: number[] = [];
  popUp: boolean = false;


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

  distanceChange(index: number, event: any) {
    const distance: number = event.target.value
    this.distances[index] = distance

  }
  data: any
  //funcion para 
  saveData() {
    this.popUp = true
    const data: sendData = {
      satelites: this.satelitesSelected,
      messages: [],
    }
    this.satelitesSelected.forEach((satelite: satelite, index: number) => {
      const messageEncrypt: MessageEncrypt = {
        sateliteIdRef: satelite.sateliteId,
        distance: this.distances[index],
        messageEncrypted: this.messagesEncrypt[index],
      }
      data.messages[index] = messageEncrypt
    });

    this.data = data

  }

  sendData() {
    this.service.connectApiPost('MessageIntersect', this.data, (res: any) => {
      if (res.status == '400' || res.status == '0') {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Error al enviar el mensaje!',
          customClass: {
            container: 'custom-alert-container'
          },
        })
      }
      else {
        Swal.fire({
          icon: 'success',
          title: 'Mensaje de ayuda enviado!',
          showConfirmButton: false,
          timer: 1500
        }).then(()=>{
          window.location.reload();
        })

      }

    })
  }

  showPopUp() {
    this.popUp = !this.popUp
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
        this.messagesEncrypt[0][count1] = ' ';
        count1 = count1 + 3
      }
      if (count2 < palabras.length) {
        this.messagesEncrypt[1][count2] = ' ';
        count2 = count2 + 2
      }
      if (count3 < palabras.length) {
        this.messagesEncrypt[2][count3] = ' ';
        count3 = count3 + 2
      }
    }
  }
}
