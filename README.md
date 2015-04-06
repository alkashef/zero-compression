
# Zero Compression #

This is a C# implementation of the zero compression encoder algorithm. It takes a stream of bytes. Each byte contains either a 0 or a 1. The output is the same as the input except for consecutive 0's or 1's. They are compressed into a single byte, whose first bit indicates if 1's or 0's were compressed and the rest of the byte (7 bits) indicate how many times it was repeated consecutively. The following diagram illustrates how the encoder works. 

![Zero Compression Encoder Illustration](https://raw.githubusercontent.com/alkashef/zerocompression/master/Diagram.png)

# Project Setup #

### Dependencies ###
This project was developed on Visual Studio 2010 and uses NUnit for unit testing.

### Project Files ###

- **README.md**: This file. It contains information about the project.
- **LICENSE.txt**: GNU GPL v3.0 License file. 
- **zeroCompression.sln**: Visual Studio Solution file.
- **Unit Tests**: The unit tests project folder.
- **zeroCompression**: The encoder project folder. 
- **Diagram.png**: A diagram illustrating how the encoder works.

### To Run the Program ###

1. Load the solution file into Visual Studio.
2. Build the solution.
3. Run the unit tests using NUnit.

# Contributors #

- Ahmad Al-Kashef: Initial implementation.

# License #

This is an open source free program provided under Version 3 of the GNU GENERAL PUBLIC LICENSE. A copy of the license is available in LICENSE.txt at the root of the source code. If not, please see <http://www.gnu.org/licenses/>.