import { Component, OnInit } from '@angular/core';
import { LogLine } from '../../interfaces/log-line';
import { LogReportService } from '../../services/logreport/log-report.service';

@Component({
  selector: 'app-shop-log',
  templateUrl: './shop-log.component.html',
  styleUrls: ['./shop-log.component.css']
})
export class ShopLogComponent implements OnInit {
  logLines: LogLine[] = [];

  constructor(private logReportService: LogReportService) { }

  ngOnInit(): void {
    this.loadLogLines();
  }

  loadLogLines(): void {
    this.logReportService.getAllLogLines().subscribe(
      (logLines: LogLine[]) => {
        this.logLines = logLines;
      },
      (error: any) => {
        console.error('Error loading log lines:', error);
      }
    );
  }

  clearLogs(): void {
    this.logReportService.clearAllLogs().subscribe(
      () => {
        this.logLines = [];
      },
      error => {
        console.error('Error clearing logs:', error);
      }
    );
  }
}
