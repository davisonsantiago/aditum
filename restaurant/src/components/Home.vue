<template>
    <div>
        <input v-model="hour" placeholder="Filter" />
        <button v-on:click="getByFilter">Search</button>
        <div id="areaGrid">
            <table v-if="arrRestaurants.length" class="tblResult">
                <thead>
                    <th>Name</th>
                    <th>Open</th>
                    <th>Close</th>
                </thead>
                <tbody>
                    <tr v-for="item in arrRestaurants" :key="item.id">
                        <td class="nameCol">{{ item.name }}</td>
                        <td>{{ item.open }}</td>
                        <td>{{ item.close }}</td>
                    </tr>
                </tbody>
              </table>
        </div>
    </div>
</template>

<script>
    import HourService from '../services/HoursService.js';
    import moment from 'moment';
    export default {
        name: 'Home',
        data(){
            return {
                hour: "",
                arrRestaurants: []
            }
        },
        methods: {
            async getByFilter() {
                this.arrRestaurants = [];
                
                try {
                    const arrResult = await HourService.getByHour(this.hour);

                    if(arrResult.length === 0){
                        alert("Nenhum registro encontrado");
                        return;
                    }

                    arrResult.forEach(element => {
                        this.arrRestaurants.push({
                            name: element.name,
                            open: moment.utc(element.open.totalMilliseconds).format('hh:mm'),
                            close: moment.utc(element.close.totalMilliseconds).format('HH:mm')
                        })
                    });   
                } catch (error) {
                    console.log(error);
                    alert(error);
                }
            }
        }
    };
</script>

<style scoped>
    #areaGrid {
        display: flex;
        width: 100%;
        margin-top: 30px;
        justify-content : center;
    }

    .tblResult{
        border-spacing: 0;
        border: 1px solid black;
    }

    .tblResult th, td{
        border:1px solid black;
        padding: 3px 20px;
        border-spacing: 0;
    }

    .nameCol{
        text-align: left;
    }
</style>

