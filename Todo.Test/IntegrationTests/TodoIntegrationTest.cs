using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.ViewModels;
using Xunit;

namespace Todo.Test.IntegrationTests
{
    public class TodoIntegrationTest : IntegrationTest
    {

        public static IEnumerable<object[]> TodoCrudTest()
        {
            List<object[]> result = new List<object[]>();
            result.Add(new object[] {
                    new UpsertTodoViewModel(){Position = 1, Title = "Things to do",
                        TodoTasks = new List<UpsertTodoTaskViewModel>() { new UpsertTodoTaskViewModel() { Position = 1, Done = false, TaskDescription = "Clean house"},
                        new UpsertTodoTaskViewModel() { Position = 2, Done = false, TaskDescription = "Buy shampoo"},
                        new UpsertTodoTaskViewModel() { Position = 3, Done = false, TaskDescription = "Buy potato"} }
                    },
                    new UpsertTodoViewModel(){Position = 1, Title = "Things to do 2",
                        TodoTasks = new List<UpsertTodoTaskViewModel>() {
                        new UpsertTodoTaskViewModel() { Position = 1, Done = false, TaskDescription = "Buy shampoo."},
                        new UpsertTodoTaskViewModel() { Position = 2, Done = false, TaskDescription = "Buy potato and eggs"} }
                    },
            });
            result.Add(new object[] {
                    new UpsertTodoViewModel(){Position = 1, Title = "Purcharse in Amazon",
                        TodoTasks = new List<UpsertTodoTaskViewModel>() {
                        new UpsertTodoTaskViewModel() { Position = 1, Done = false, TaskDescription = "Buy tv"},
                        new UpsertTodoTaskViewModel() { Position = 2, Done = false, TaskDescription = "Buy phone"}}
                    },
                    new UpsertTodoViewModel(){Position = 1, Title = "Things to do 2",
                        TodoTasks = new List<UpsertTodoTaskViewModel>() {
                        new UpsertTodoTaskViewModel() { Position = 1, Done = false, TaskDescription = "Buy tv"},
                        new UpsertTodoTaskViewModel() { Position = 2, Done = false, TaskDescription = "Buy phone"},
                        new UpsertTodoTaskViewModel() { Position = 1, Done = false, TaskDescription = "Buy monitor"}}
                    },
            });
            return result;
        }

        [Theory]
        [MemberData(nameof(TodoCrudTest))]
        public async Task TodoCrudCase(UpsertTodoViewModel createTodoViewModel, UpsertTodoViewModel updateTodoViewModel)
        {
            string api = "todo";
            string upsertStringResponse = await PostWithResponse(testhttpClient, api, createTodoViewModel);
            JObject upsertJsonObject = JObject.Parse(upsertStringResponse);
            TodoResponseViewModel upsertResponse = upsertJsonObject.ToObject<TodoResponseViewModel>();

            MappingId(createTodoViewModel, upsertResponse);
            MappingId(updateTodoViewModel, upsertResponse);
            //GetbyId
            string getJson = await Get(testhttpClient, api + "/" + upsertResponse.Id);
            JObject getJsonObject = JObject.Parse(getJson);
            TodoResponseViewModel getResponse = getJsonObject.ToObject<TodoResponseViewModel>();

            AssertEquals(createTodoViewModel, getResponse);
            
            //Update
            upsertStringResponse = await PutWithResponse(testhttpClient, api, updateTodoViewModel);
            upsertJsonObject = JObject.Parse(upsertStringResponse);
            upsertResponse = upsertJsonObject.ToObject<TodoResponseViewModel>();

            ////GetAll
            getJson = await Get(testhttpClient, api);
            List<TodoResponseViewModel> todoResponseViewModels = JArray.Parse(getJson).ToObject<List<TodoResponseViewModel>>();
            //Mapping again in case i Added extra todotask.
            MappingId(updateTodoViewModel, upsertResponse);
            AssertEquals(upsertResponse, todoResponseViewModels.Where(x => x.Id == upsertResponse.Id).FirstOrDefault());


            //Delete 
            await Delete(testhttpClient, api  + "/" + upsertResponse.Id);

            //GetbyId
            getJson = await Get(testhttpClient, api + "/" + upsertResponse.Id);
            getJsonObject = JObject.Parse(getJson);
            getResponse = getJsonObject.ToObject<TodoResponseViewModel>();
            Assert.False(getResponse.Deleted == null);

        }

       

        private void MappingId(UpsertTodoViewModel upsertTodoViewModel, TodoResponseViewModel todoResponseViewModel)
        {
            upsertTodoViewModel.Id = todoResponseViewModel.Id;
            for (int i = 0; i < upsertTodoViewModel.TodoTasks.Count(); i++)
            {
                if (i < upsertTodoViewModel.TodoTasks.Count() && i < todoResponseViewModel.TodoTasks.Count())
                    upsertTodoViewModel.TodoTasks[i].Id = todoResponseViewModel.TodoTasks[i].Id;

            }
        }
    }
}


