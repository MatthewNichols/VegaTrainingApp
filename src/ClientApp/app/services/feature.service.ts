import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class FeatureService {

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	getFeatures() {
        return this.http.get(this.originUrl + '/api/features')
			.map(res => res.json());
	}
}