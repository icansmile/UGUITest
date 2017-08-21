

-- 输出
print("test")

print("------------------全局变量不需要声明,返回nil-------------------")
print(a)
a = 10
print(a)

print("------数据类型:nil, boolean, number, string, function, userdata, thread, table---------------------------")
print(type(10))
print(type(10.3*1))

string1 = "string 1]]"
string2 = [[@ksadfj#[]kj]]
print(string1)
print(string2)

string3 = "字符串连接".."a".."b"
string4 = "3" + "2"
print(string3)
print(string4)

local table1 = {}
local table2 = {"a", "b"}
table2["b"] = 2
table2["c"] = 3
table2["d"] = 3

for k,v in pairs(table2) do
    print(k, v)
end
print("==================")
for i = 1, 10 do
    print(i,table2[i])
end

print("===========function==============")
function testFunc(tab, func)
    for k,v in pairs(tab) do
        print(func(k, v))
    end
end

function testFunc2(k, v)
    print(k, v)
end

tab = {key1 = "val1", key2 = "key2"}
print("=========lamba function=========")
testFunc(tab, 
    function(k,v)
        print(k, v)
    end
)
print("=========function2=========")
testFunc(tab, testFunc2)

print("=========swap=========")
local x = 1
local y = 2
x, y = y, x
print("x="..x, "y="..y)

print("=========get tab=========")
print(tab.key1)