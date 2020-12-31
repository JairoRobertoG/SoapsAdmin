import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  soapId: number = 0;

  constructor(private route: ActivatedRoute) {

  }

  public ngOnInit(): void {
    this.route.params.subscribe(result => {
      console.log(result.id);
      this.soapId = Number(result.id);
    });
  }

  public incrementCounter() {
    this.currentCount++;
  }
}
