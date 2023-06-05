using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using InvtryMgtSystemAPI.Controllers;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvntryMgtSystemApp.Tests.Controller
{
    public class CategoryControllerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryControllerTests()
        {
            _categoryRepository = A.Fake<ICategoryRepository>();
            _mapper=A.Fake<IMapper>();
        }

        [Fact]

        public void CategoryController_GetCategories_ReturnOk()
        {
            //Arrange
            var categories = A.Fake<ICollection<CategoryDto>>();
            var categoryList = A.Fake<List<CategoryDto>>();

            A.CallTo(() => _mapper.Map<List<CategoryDto>>(categories)).Returns(categoryList);

            var controller=new CategoryController(_categoryRepository, _mapper);

            //Act

            var result = controller.GetCategories();

            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]

        public void CategoryController_CreateCategory_RetunOk()
        {
            var category = A.Fake<Category>();
            var categoryMap = A.Fake<Category>();
            var categoryCreate = A.Fake<CategoryDto>();
            var categories = A.Fake<ICollection<CategoryDto>>();
            var categoryList = A.Fake<List<CategoryDto>>();
            A.CallTo(() => _mapper.Map<List<CategoryDto>>(categoryCreate)).Returns(categoryList);

            A.CallTo(() => _categoryRepository.GetCategoryTrimToUpper(categoryCreate)).Returns(category);

            A.CallTo(() => _categoryRepository.CreateCategory(category) );

            var controller = new CategoryController(_categoryRepository, _mapper);

            //Act

            var result = controller.CreateCategory(categoryCreate);

            //Assert

            result.Should().NotBeNull();

            //result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
