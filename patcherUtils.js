const fs = require('fs');
const p = require('path');

if(!fs.existsSync('www/texturepacks')){
	fs.mkdirSync('www/texturepacks');
}

if(!fs.existsSync('www/js/customScripts')){
	fs.mkdirSync('www/js/customScripts');
}

try{
	window.pconf = JSON.parse(fs.readFileSync('www/patcher.json'));
}catch(_){
	window.pconf = {
		texture_pack: 'none',
		keys: {
			PAUSE: 'p',
			SAVE: 's',
			RESTART: 'r',
			JS_CONSOLE: 'j',
			GOD_MODE: 'g',
			ADD_MONEY: 'm',
			NO_CLIP: 'n',
			SET_SPEED: 'e',
			SET_TEXTURE_PACK: 't',
			HELP: 'h'
		}
	}
	fs.writeFileSync('www/patcher.json', JSON.stringify(pconf, null, 4));
}

window.loadedTextures = [];
window.godMode = false;
let noclip = false;

window.addEventListener('load', ()=>{
	fs.readdirSync('www/js/customScripts').forEach(f=>{
		if(!f.endsWith('.js')) return;
		let s = document.createElement('script');
		s.src = 'js/customScripts/' + f;
		document.body.appendChild(s);
	});
});

window.addEventListener('keypress', e=>{
	switch(e.key){
		case pconf.keys.JS_CONSOLE: {
			let inp = prompt('Введите команду');
			if(!inp || inp.length == 0) return;
			try{
				alert(eval(inp));
			}catch(e){
				alert(e);
			}
			break;
		}

		case pconf.keys.SAVE: {
			SceneManager.push(Scene_Save);
			break;
		}

		case pconf.keys.GOD_MODE: {
			window.godMode = true;
			alert('Режим бога включен');
			break;
		}

		case pconf.keys.ADD_MONEY: {
			$gameParty._gold = Number.MAX_SAFE_INTEGER;
			alert('Деньги накручены');
			break;
		}

		case pconf.keys.PAUSE: {
			alert('ПАУЗА\n\nНажмите ОК, чтобы возобновить');
			break;
		}

		case pconf.keys.RESTART: {
			location.reload();
			break;
		}

		case pconf.keys.NO_CLIP: {
			noclip = !noclip;
			if(noclip){
				$gamePlayer.rCanPass = $gamePlayer.canPass;
				$gamePlayer.canPass = ()=>true;
			}else{
				$gamePlayer.canPass = $gamePlayer.rCanPass;
			}
			break;
		}

		case pconf.keys.SET_SPEED: {
			let inp = prompt('Введите новую скорость', $gamePlayer._moveSpeed);
			if(inp && !isNaN(+inp)){
				$gamePlayer._moveSpeed = +inp;
			}
			break;
		}

		case pconf.keys.SET_TEXTURE_PACK: {
			texturePacksUI();
			break;
		}

		case pconf.keys.HELP: {
			let str = 'Клавиши RPGM Patcher\n\n';
			str += `[${pconf.keys.PAUSE.toUpperCase()}] Поставить игру на паузу\n`;
			str += `[${pconf.keys.RESTART.toUpperCase()}] Перезапустить игру\n`;
			str += `[${pconf.keys.JS_CONSOLE.toUpperCase()}] Открыть консоль JavaScript\n`;
			str += `[${pconf.keys.SAVE.toUpperCase()}] Открыть окно сохранения\n`;
			str += `[${pconf.keys.SET_TEXTURE_PACK.toUpperCase()}] Открыть окно выбора текстурпака\n`;
			str += `[${pconf.keys.GOD_MODE.toUpperCase()}] Режим бога (очень высокие HP, атака, защита и т.д.)\n`;
			str += `[${pconf.keys.ADD_MONEY.toUpperCase()}] Накрутка денег\n`;
			str += `[${pconf.keys.NO_CLIP.toUpperCase()}] NoClip (возможность проходить сквозь объекты)\n`;
			str += `[${pconf.keys.SET_SPEED.toUpperCase()}] Изменить скорость игрока\n`;
			alert(str);
		}
	}
});

CSSStyleDeclaration.prototype.set = function(obj){
	for(let i in obj){
		this[i] = obj[i];
	}
}

String.prototype.replaceAll = function(o, n){
	return this.split(o).join(n);
}

function popup(opts){
	if(!opts) opts = {};

	let h = document.createElement('div');
	h.style.set({
		position: 'absolute',
		left: '0',
		top: '0',
		width: '100vw',
		height: '100vh',
		zIndex: '999999',
		display: 'flex',
		justifyContent: 'center',
		alignItems: 'center'
	});
	h.addEventListener('click', e=>{
		if(e.target == h){
			document.body.removeChild(h);
			if(opts.onclose) opts.onclose();
		}
	});

	let box = document.createElement('div');
	box.style.set({
		border: 'solid 1px black',
		padding: '1em',
		background: 'white',
		cursor: 'default',
		fontFamily: 'Consolas, monospace'
	});

	h.appendChild(box);

	return { h: h, box: box, show: ()=>{ document.body.appendChild(h) }, close: ()=>{ document.body.removeChild(h) } };
}

function readDir(dir){
	let arr = [];

	fs.readdirSync(dir).forEach(f=>{
		let n = p.join(dir, f);
		if(fs.statSync(n).isDirectory()){
			arr.push(...readDir(n));
		}else{
			arr.push(n);
		}
	});

	return arr;
}

function getSubDirs(dir){
	let arr = [];

	fs.readdirSync(dir).forEach(f=>{
		let n = p.join(dir, f);
		if(fs.statSync(n).isDirectory()){
			arr.push(n);
			arr.push(...getSubDirs(n));
		}
	});

	return arr;
}

function texturePacksUI(){
	let { box, show, close } = popup();
	
	let header = document.createElement('h2');
	header.style.margin = '0 0 0.5em 0.5em';
	header.innerText = 'Текстурпаки';
	
	let sel = document.createElement('select');
	sel.style.set({ width: '20em', marginBottom: '1em' });
	sel.size = 7;
	sel.options[0] = new Option('<нет>', 'none');
	sel.focus();
	
	fs.readdirSync('www/texturepacks').forEach((d,i)=>{
		sel.options[i+1] = new Option(d, d);
	});
	sel.value = pconf.texture_pack;

	let btnSel = document.createElement('button');
	btnSel.style.set({ padding: '0.2em 0.5em', marginRight: '0.5em' });
	btnSel.innerText = 'Применить';

	btnSel.addEventListener('click', ()=>{
		pconf.texture_pack = sel.value;
		fs.writeFileSync('www/patcher.json', JSON.stringify(pconf, null, 4));
		location.reload();
	});

	let btnCreate = document.createElement('button');
	btnCreate.style.padding = '0.2em 0.5em';
	btnCreate.innerText = 'Создать новый';

	btnCreate.addEventListener('click', ()=>{
		let name = prompt('Введите название нового текстурпака', document.title+'_New1');
		if(name && name.length > 0){
			fs.mkdirSync('www/texturepacks/' + name);
			decryptGameFiles('texturepacks/' + name, ()=>{
				close();
				texturePacksUI();
			});
		}
	});

	box.appendChild(header);
	box.appendChild(sel);
	box.appendChild(document.createElement('br'));
	box.appendChild(btnSel);
	box.appendChild(btnCreate);
	show();
}

function decryptGameFiles(destDir, onend){
	let { box, show } = popup({ onclose: onend });

	let header = document.createElement('h2');
	header.style.margin = '0 0 0.5em 0.5em';
	header.innerText = 'Создание текстурпака';

	let logt = document.createElement('textarea');
	logt.readOnly = true;
	logt.style.set({ width: '30em', height: '10em' });

	box.appendChild(header);
	box.appendChild(logt);
	show();

	function log(text){
		logt.value += text + '\n';
		logt.scrollTo(0, logt.scrollHeight);
	}

	log('Создание папок...');
	getSubDirs('www/img').forEach(d=>{
		let dr = d.replaceAll('\\', '/').replace('img', destDir);
		if(!fs.existsSync(dr)){
			fs.mkdirSync(dr);
			log('Создана папка: ' + dr);
		}
	});

	let files = readDir('www/img');
	if(files.find(n=>n.endsWith('.rpgmvp'))){
		log('Текстуры зашифрованы. Расшифровка и копирование...');

		let ptp = pconf.texture_pack;
		pconf.texture_pack = 'none';

		function df(i){
			let n = files[i];

			if(!n.endsWith('.rpgmvp')){
				let dst = n.replace('img', destDir);
				fs.copyFileSync(n, dst);
				log(`Файл ${n} скопирован в ${dst}`);
				if(i+1 < files.length){
					df(i+1);
				}else{
					log('Текстурпак создан');
				}
				return;
			}

			log('Расшифровка файла: ' + n);

			let nm = n.replaceAll('\\', '/').replace('www/', '').replace('.rpgmvp', '.png');

			let img = new Image();
			let bitmap = { _image: img, _onLoad: ()=>{}, _onError: ()=>{}, _renewCanvas: ()=>{} };
			Decrypter.decryptImg(nm, bitmap);

			let dst = n.replaceAll('\\', '/').replace('img', destDir).replace('.rpgmvp', '.png');
			img.onload = ()=>{
				let cnv = document.createElement('canvas');
				cnv.width = bitmap._image.naturalWidth;
				cnv.height = bitmap._image.naturalHeight;
				cnv.getContext('2d').drawImage(bitmap._image, 0, 0, bitmap._image.naturalWidth, bitmap._image.naturalHeight);

				let data = cnv.toDataURL().split(',').pop();
				fs.writeFileSync(dst, data, { encoding: 'base64' });
				log(`Файл ${n} расшифрован и скопирован в ${dst}`);

				if(i+1 < files.length){
					df(i+1);
				}else{
					log('Текстурпак создан');
				}
			}
		}

		df(0);
		pconf.texture_pack = ptp;
	}else{
		log('Текстуры не зашифрованы. Копирование...');
		files.forEach(n=>{
			let nm = n.replace('img', destDir);
			fs.copyFileSync(n, nm);
			log(`Скопирован файл: из ${n} в ${nm}`);
		});
		log('Текстурпак создан');
	}
}