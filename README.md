# nuget Global.Common.Features.Models
# namespace Global.Common.Models

# Description

# This project contains several features that provide functionalities to support and enhance development.
# One important feature is that it contains a generic responses hierarchy, which allows you to manage your development in the most common scenarios.
# This is a beta version and has not been thoroughly tested or recommended for use in production environments.


# Dependencies
# https://www.nuget.org/packages/Global.Common.Features

# Features

## Generic responses hierarchy

- GlobalResponse
	- GlobalResponse<TEx>
		- ExGlobalResponse
		- GlobalValueResponse<T, TEx>
			- ExGlobalValueResponse<T>