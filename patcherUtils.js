const fs = require('fs');

if(!fs.existsSync('www/texturepacks')){
	fs.mkdirSync('www/texturepacks');
}

try{
	window.pconf = JSON.parse(fs.readFileSync('patcher.json'));
}catch(_){
	fs.writeFileSync('patcher.json', '{\r\n    texture_pack: "none"\r\n}');
	window.pconf = { texture_pack: 'none' };
}

window.loadedTextures = [];
window.godMode = false;

window.onkeypress = e=>{
	if(e.key == 'j'){
		let inp = prompt('Введите команду');
		if(!inp || inp.length == 0) return;
		try{
			alert(eval(inp));
		}catch(e){
			alert(e);
		}
	}else if(e.key == 's'){
		SceneManager.push(Scene_Save);
	}else if(e.key == 'g'){
		window.godMode = true;
		alert('Режим бога включен');
	}else if(e.key == 'm'){
		$gameParty._gold = Number.MAX_SAFE_INTEGER;
		alert('Деньги накручены');
	}else if(e.key == 'p'){
		alert('ПАУЗА\n\nНажмите ОК, чтобы возобновить');
	}else if(e.key == 'h'){
		alert('Горячие клавиши RPGM Patcher\n\n[P] Поставить игру на паузу\n[R] Перезапустить игру\n[T] Открыть окно выбора текстурпака\n[J] Открыть консоль JavaScript\n[S] Открыть окно сохранения\n[G] Режим бога (очень высокие HP и атака)\n[M] Накрутка денег');
	}else if(e.key == 'b'){
		alert(loadedTextures.join('\n'));
	}else if(e.key == 'r'){
		location.reload();
	}else if(e.key == 't'){
		let p = prompt('Введите название текстурпака', pconf.texture_pack);
		if(!p) return;
		pconf.texture_pack = p;
		fs.writeFileSync('patcher.json', JSON.stringify(pconf, null, 4));
		location.reload();
	}
};