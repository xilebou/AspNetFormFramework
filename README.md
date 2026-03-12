# IMPORTANT! This is a learning project. Not to be used in production.


## Installation && Setup
Download this solution using ```git clone https://github.com/xilebou/AspNetFormFramework.git```
Then, Create your own project in the same solution.
Finally, reference the framework in your project.


## Usage
Create a Form Model in the Form directory at the root of your project using the ```[Form]``` attribute.
To Map the endpoints of your forms automatically:

### Program.cs file
```
FormMapper mapper = new FormMapper(webApplication); //pass in your web application
mapper.MapForms(typeof()) // the type of your controller
```

### Controller file
There is the interface IFormController if you want to implement your own controller and mappings. However, you might find it easier to use the ```BaseFormController``` class. This class streamlines the endpoint creation, however its approach is a lot less flexible.

### Form files
Use the ```[Form]``` attribute on a class to create a form in your "Form" directory.
By default, the name of the form is the class name. You can specify the name of the form with ```[Form(Name = "MyCustomForm")]```.
By default also, the uri of the form is the controller name + the name of the class. For example: ```http://localhost/test/myCustomForm```.

The input types of the form can be specified with their type. For example A string is a text input.
You can also specify the input type yourself (if you need a password field, for example) ```[Input(Type = "password")]```
#### Don't forget to add getters and setters for your inputs!
| Html input type       | Default matching type |
| :---        |    :----:   |
| text      | string       |
| password   | N/A        |
| number     | int      |
| decimal    | double   |


## Examples

A form might look like this

```
[Form(Name = "Form#42")]
public class MyCustomForm
{
    [Form.Input(Label = "Enter your Name: ", InputType = "text")]
    public string? Name { get; set; }

    public string? YourFirstName { get; set; }

    [Form.Input(InputType = "password")]
    public int? YourNewPin { get; set; }
}
```

## Limitations
It's really not production ready. There is no validation of which form you want to get.
Then, there's almost no security, no authorization, no nothing! it's the far west. You could add a middleware to fix it, but it won't fix the underlying issues.
Also, reflection is used a where it should really not be. It's not catastrophic but deserves a refactor.
Finally, there are other frameworks that do this but better. 

## What I want to do in the future

I'd like to make the project more robust and maybe eventually production ready. For now, security and validation are the biggest issues.

