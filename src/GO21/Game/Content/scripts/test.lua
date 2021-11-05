import("GO21Engine.Engine")

print("TEST.LUA =")

function OnAdded(list)
    tex = Engine.LoadTex("penguin")
    print("tex: " .. tex)
end

function Draw()
    Engine.Drawing.Texture(tex, 10, 10)
end