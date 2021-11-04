import axios from 'axios';

const env = require('../../prod.env');

export default {

	async getByHour(hour) {
		const apiPath = env.API_PATH;

		const response = await axios.get(apiPath + "/hours/" + hour);

		return response.data;
	}
}