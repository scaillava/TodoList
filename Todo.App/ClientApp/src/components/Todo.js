import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class Todo extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
      this.state = { todolists: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderTodoListTable(todolists) {
      return ({
          todolists.map(todolist =>
              <div id="myDIV" class="header">
                  <h2>My To Do List</h2>
                  {/*<input type="text" id="myInput" placeholder="Title...">*/}
                      <span onclick="newElement()" class="addBtn">Add</span>
                </div>
              <ul id="myUL">
                {todolist.todochecks.map(task =>
                    <li>task.taskDescription</li>
                )}
                  </ul>
           
          )}
       
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const token = await authService.getAccessToken();
    const response = await fetch('api/todo', {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ todolists: data, loading: false });
  }
}
