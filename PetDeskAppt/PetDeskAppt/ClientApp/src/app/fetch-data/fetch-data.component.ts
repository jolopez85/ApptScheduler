import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public appointments: Appointment[];
  public selectedAppointment: Appointment;
  public message: IMessage = {header: "tes", description: "test"};
  public showMessage = false;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient.get<Appointment[]>('appointments')
                  .subscribe(result => {
                    this.appointments = result;
                  },
                  error => console.log('error occurred while fetching data', error)
    );

  }

  public ViewDetail(id: string) : void {

    const appointment = this.appointments.find(t => t.id == Number.parseInt(id || "0"));
    this.selectedAppointment = appointment;
    document.getElementById('modal').style.display = 'block'
    console.log(appointment);
   
  }

  public Confirm(){
    this.hideModal();
      this.httpClient.post<IBaseResponse>('appointments/confirm',{})
                     .subscribe(result => {
                        this.showMessage = result != null;
                        this.message.header = "Schedule confirmed!";
                        this.message.description = `Thanks for submitting your confirmation, you service ${this.selectedAppointment != null ? this.selectedAppointment.type : ""} has been confirmed by ${this.selectedAppointment != null ?this.selectedAppointment.requestedDateTimeOffset:""}. Confirmation id ${result.confirmationId}`;
                     },
                     error => console.log('error while confirming appointment'));
            
  }

  public Reschedule(id: string) {
    this.hideModal();
    const appointmentId = Number.parseInt(id);

    const appointment = this.appointments.find(t => t.id === appointmentId);

    this.httpClient.post<IAppointmentUpdate>('appointments/update', appointment)
                   .subscribe(result => {
                      this.showMessage = result != null;
                      const index = this.appointments.findIndex(t => t.id === appointmentId)
                      const previousDate = appointment.requestedDateTimeOffset;
                      appointment.requestedDateTimeOffset = result.appointmentDate;
                      this.appointments[index] = appointment;
                      this.message.header = "Service Rescheduled!";
                      this.message.description = `Your ${this.selectedAppointment.type || ""} service for ${result.petName} has been updated from ${previousDate} to ${result.appointmentDate}`;
                      this.hideModal();
                    },
                    error => console.log('error while confirming appointment'));
  }

  public hideModal(){
    document.getElementById('modal').style.display = 'none'
  }

  public hideMessage(){
    this.showMessage = false;
  }
  

}


interface Appointment {
  id: number,
  createDateTime: string,
  requestedDateTimeOffset: string,
  type: string,
  user: IUser,
  animal: IAnimal
}

interface IUser {
  userId: number,
  firstName: string,
  lastName: string,
  vetDataId: string
}

interface IAnimal {
   animalId: number,
   firstName: string,
   species: string,
   breed: string
}

interface IAppointmentUpdate{
   petName : string,
   appointmentDate : string
}

interface IMessage{
  header: string,
  description: string
}


interface IBaseResponse{
  sucess: boolean,
  message: string,
  confirmationId: string
}

