
import { Component, ViewChild, Output, EventEmitter } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ProjectServiceService } from 'src/app/Services/project-service.service';
//'MessageIntersect/getAllMessagesIntersected'
@Component({
  selector: 'app-rebel-console',
  templateUrl: './rebel-console.component.html',
  styleUrls: ['./rebel-console.component.css']
})
export class RebelConsoleComponent {
  @Output() reloadData = new EventEmitter()
  dataSource: MatTableDataSource<any>;


  constructor(private service: ProjectServiceService, ) {
    this.dataSource = new MatTableDataSource;

    
  }
  public displayedColumns: string[] = []
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  @ViewChild(MatSort)
  sort: MatSort = new MatSort;


  public async get() {
    await this.service.connectApiGet('MessageIntersect/getAllMessagesIntersected', async (res: any) => {
      console.log(res.body)
      var resp = res.body
    

      // Guardar el estado de ordenamiento actual
  

      this.dataSource.data = resp;
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      const hiddenColumns = ['decryptedMessageId']; // lista de columnas a ocultar
      for (let index = 0; index < resp?.length; index++) {
        let date = resp[index].auditDate.split('T')
        resp[index].auditDate = date
        this.loadTable([resp[index]], hiddenColumns);
      }
      // Ordenar por consecutivo de mayor a menor
      this.dataSource.sort?.sort({
        id: 'consecutive',
        start: 'desc',
        disableClear: false
      });
      // Restaurar el estado de ordenamiento
   
    });
  }
  loadTable(data: any[], hiddenColumns: string[]) {
    console.log('aca1',data)
    this.displayedColumns = [];
    for (let column in data[0]) {
      if (hiddenColumns.indexOf(column) === -1) { // si la columna no está en la lista de ocultas
        this.displayedColumns.push(column);
      }
    }
    

  }
  ngOnInit(): void {
    this.get();
    console.log('aca',this.dataSource)
    console.log('aca',this.displayedColumns)
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
